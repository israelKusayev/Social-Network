import React from 'react';
import Modal from 'react-bootstrap4-modal';
import Comment from './comment';
import ImagePicker from './imagePicker';
import { addComment, getComments } from '../services/commentsService';
import { toast } from 'react-toastify';
import { unlikeComment, likeComment } from '../services/likesService';

export default class Comments extends React.Component {
  socialUrl = process.env.REACT_APP_SOCIAL_URL;

  componentDidMount = async () => {
    console.log(this.props.postId);

    const res = await getComments(this.props.postId);
    if (res.status !== 200) {
      console.log('getCommentsFaild.  response-', res);
    } else {
      const data = await res.json();
      this.setState({ comments: data });
    }
  };

  state = {
    error: '',
    data: {
      content: '',
      image: ''
    },
    comments: [
      {
        commentId: '123f45678',
        imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
        numberOfLikes: 23,
        isLiked: true,
        User: { UserName: 'israel', UserId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '20 minute ago'
      }
    ]
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

  handleChange = ({ currentTarget: input }) => {
    const data = { ...this.state.data };
    data[input.id] = input.value;
    this.setState({ data });
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
        this.setState({ error: '' });
        toast.success('comment added successfully');
      }
    }
  };
  handleImageSelect = (image) => {
    const data = { ...this.state.data };
    data.image = image;
    this.setState({ data });
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
