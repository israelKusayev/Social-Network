import React from 'react';
import Modal from 'react-bootstrap4-modal';
import Comment from './comment';
import ImagePicker from './imagePicker';
import { addComment, getComments } from '../services/commentsService';
import { toast } from 'react-toastify';
import { unlikeComment, likeComment } from '../services/likesService';
import { getUsers } from '../services/usersService';
import { onReferenceSelect } from '../utils/referencing';

export default class Comments extends React.Component {
  socialUrl = process.env.REACT_APP_SOCIAL_URL;

  componentDidMount = async () => {
    await this.getComments();
  };
  getComments = async () => {
    const res = await getComments(this.props.postId);
    if (res.status !== 200) {
      toast.error('something went wrong');
    } else {
      const data = await res.json();
      
      this.setState({ comments: data });
    }
  };

  state = {
    error: '',
    data: {
      content: '',
      image: '',
      referencing: []
    },
    comments: [],
    getAutoComplete: false,
    users: []
  };

  onLiked = async (commentId) => {
    const prevState = JSON.parse(JSON.stringify(this.state));

    const comments = [...this.state.comments];
    const comment = comments.find((p) => p.commentId === commentId);
    comment.isLiked = !comment.isLiked;
    comment.numberOfLikes = comment.isLiked ? ++comment.numberOfLikes : --comment.numberOfLikes;
    this.setState({ comments });

    if (comment.isLiked) {
      //like comment
      const res = await likeComment(comment.commentId);
      if (res.status !== 200) {
        this.setState({ ...prevState });
      }
    } else {
      //unlike comment
      const res = await unlikeComment(comment.commentId);
      if (res.status !== 200) {
        this.setState({ ...prevState });
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

  onReferencingSelect = (user) => {
    const data = this.state.data;
    const res = onReferenceSelect(user, data.content);
    data.referencing.push(res.reference);
    data.content = res.content;
    this.setState({ data, users: [], getAutoComplete: false });
  };

  addComment = async (e) => {
    e.preventDefault();
    if (!this.state.data.content.trim()) {
      this.setState({ error: 'content is required!' });
    } else {
      const res = await addComment(this.state.data, this.props.postId);
      if (res.status !== 200) {
        this.setState({ error: 'add comment faild please try again!' });
      } else {
        this.resetAddComment();
        await this.getComments();
      }
    }
  };
  handleImageSelect = (image) => {
    const data = { ...this.state.data };
    data.image = image;
    this.setState({ data });
  };

  resetAddComment = () => {
    let data = this.state.data;
    data.content = '';
    data.image = '';
    data.referencing = [];
    this.setState({ data, error: '' });
  };

  render() {
    const { error, data, comments } = this.state;

    return (
      <Modal dialogClassName="modal-lg " visible={this.props.isVisible} onClickBackdrop={this.modalBackdropClicked}>
        <div className="bg-dark text-white">
          <div className="modal-header">
            <h5 className="modal-title">Comments</h5>
            <button className="btn" onClick={this.props.onClose}>
              <i className="fa fa-times text-pink" />
            </button>
          </div>
          <div className="modal-body ">
            <div className="card bg-dark mb-4">
              <div className="card-body">
                <form onSubmit={this.addComment}>
                  <h5>Add comment</h5>
                  <div className="form-group">
                    <textarea
                      type="text"
                      className="form-control"
                      placeholder="content"
                      value={data.content}
                      onChange={this.handleChange}
                      id="content"
                      required={true}
                    />
                    <div className="autocomplete-items" id="autocomplete-list">
                      {this.state.users.map((p) => {
                        return (
                          <div onClick={() => this.onReferencingSelect(p)} className="alert alert-dark">
                            {p.UserName}
                          </div>
                        );
                      })}
                    </div>
                  </div>
                  {error && <div classNmae="alert alert-danger">{error}</div>}
                  <div className="form-group ">
                    <ImagePicker onUpload={this.handleImageSelect} />
                  </div>
                  <button type="submit" className="btn btn-pink">
                    Add
                  </button>
                </form>
              </div>
            </div>
            {comments.map((comment) => {
              return <Comment key={comment.commentId} onLiked={this.onLiked} comment={comment} />;
            })}
          </div>
          <div className="modal-footer">
            <button type="button" className="btn btn-pink" onClick={this.props.onClose}>
              Close
            </button>
          </div>
        </div>
      </Modal>
    );
  }
}
