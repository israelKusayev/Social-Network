import React, { Component } from 'react';
import ImagePicker from '../components/imagePicker';
import RouteProtector from '../HOC/routeProtector';
import { Post } from '../services/httpService';

class CreatePost extends Component {
  socialUrl = process.env.REACT_APP_SOCIAL_URL;
  state = {
    data: {
      whoIsWatching: 'all',
      content: '',
      image: ''
    }
  };

  handleChange = ({ currentTarget: input }) => {
    const data = { ...this.state.data };
    data[input.id] = input.value;
    this.setState({ data });
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
    console.log(this.state);

    const res = await Post(`${this.socialUrl}createPost`, this.state.data);
    if (res.status === 200) {
      this.props.history.push('/');
    } else {
      this.setState({ error: 'somthing went worng please try again' });
    }
  };
  render() {
    const { data } = this.state;
    return (
      <div className="container mt-1">
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
                      <div className="form-group row">
                        <label htmlFor="content" className="col-4 col-form-label">
                          Content
                        </label>
                        <div className="col-8">
                          <textarea
                            id="content"
                            placeholder="Content"
                            cols="60"
                            rows="7"
                            className="form-control"
                            required={true}
                            value={data.content}
                            onChange={this.handleChange}
                          />
                        </div>
                      </div>
                      <div className="form-group row">
                        <label className="col-4 col-form-label">Image</label>
                        <div className="col-8">
                          <ImagePicker onUpload={this.handleImageSelect} />
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
