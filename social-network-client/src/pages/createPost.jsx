import React, { Component } from 'react';
import ImagePicker from '../components/imagePicker';
import RouteProtector from '../HOC/routeProtector';
import { createPost } from '../services/postsService';
import { getUsers } from '../services/usersService';
import { toast } from 'react-toastify';
import { onReferenceSelect } from '../utils/referencing';

class CreatePost extends Component {
  state = {
    data: {
      whoIsWatching: 'all',
      content: '',
      image: '',
      referencing: []
    },
    users: [],
    getAutoComplete: false
  };

  getUsers = async (value) => {
    if (value.trim()) {
      const res = await getUsers(value);
      if (res.status !== 200) {
        toast.error('something went wrong...');
      } else {
        const users = await res.json();
        if (users.length === 0) this.setState({ users, getAutoComplete: false });
        else this.setState({ users });
      }
    }
  };

  handleChange = async ({ currentTarget: input }) => {
    const data = { ...this.state.data };
    data[input.id] = input.value;
    this.setState({ data });

    if (input.value.endsWith('@')) {
      this.setState({ getAutoComplete: true });
    }
    if (this.state.getAutoComplete) {
      const contentArr = input.value.split('@');
      await this.getUsers(contentArr[contentArr.length - 1]);
    }
  };

  handleImageSelect = (image) => {
    const data = { ...this.state.data };
    data.image = image;
    this.setState({ data });
  };

  handleSubmit = async (e) => {
    e.preventDefault();

    if (!this.state.data.content.trim()) {
      this.setState({ error: 'content is required!' });
      return;
    }
    const res = await createPost(this.state.data);
    if (res.status === 200) {
      this.props.history.push('/');
    } else {
      this.setState({ error: 'somthing went worng please try again' });
    }
  };

  onReferencingSelect = (user) => {
    const data = this.state.data;
    const res = onReferenceSelect(user, data.content);
    data.referencing.push(res.reference);
    data.content = res.content;
    this.setState({ data, users: [], getAutoComplete: false });
  };

  render() {
    const { data } = this.state;
    return (
      <div className="container mt-3">
        <div className="row">
          <div className="col-md-8 offset-2">
            <div className="card bg-dark text-white">
              <div className="card-body">
                <div className="row">
                  <div className="col-md-12">
                    <h4>Create post</h4>
                    <hr />
                  </div>
                </div>
                <div className="row">
                  <div className="col-md-12">
                    <form onSubmit={this.handleSubmit}>
                      <div className="form-group row">
                        <label htmlFor="bio" className="col-4 col-form-label">
                          Who can see your story?
                        </label>
                        <div className="col-8">
                          <select
                            value={data.whoIsWatching}
                            onChange={this.handleChange}
                            className="form-control"
                            id="whoIsWatching"
                          >
                            <option value="all">All</option>
                            <option value="followers">Only my followers</option>
                          </select>
                        </div>
                      </div>
                      <div className="form-group  row">
                        <label htmlFor="content" className="col-4 col-form-label">
                          Content
                        </label>
                        <div className="col-8 autocomplete">
                          <textarea
                            id="content"
                            placeholder="Content"
                            cols="60"
                            rows="7"
                            className="form-control "
                            required={true}
                            value={data.content}
                            onChange={this.handleChange}
                          />
                          <div className="autocomplete-items" id="autocomplete-list">
                            {this.state.users.map((p) => {
                              return (
                                <div
                                  onClick={() => this.onReferencingSelect(p)}
                                  key={p.UserId}
                                  className="alert alert-dark"
                                >
                                  {p.UserName}
                                </div>
                              );
                            })}
                          </div>
                        </div>
                      </div>
                      <div className="form-group row">
                        <label className="col-4 col-form-label">Image</label>
                        <div className="col-8">
                          <ImagePicker onUpload={this.handleImageSelect} image = {this.state.data.image} />
                        </div>
                      </div>

                      <div className="form-group row">
                        <div className="offset-4 col-8">
                          <button name="submit" type="submit" className="btn btn-pink">
                            Share
                          </button>
                        </div>
                      </div>
                    </form>
                    {this.state.error && <div className="alert alert-danger">{this.state.error} </div>}
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default RouteProtector(CreatePost);
