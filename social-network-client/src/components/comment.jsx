import React, { Component } from 'react';
import LikeButton from './likeButton';

export default class Comment extends Component {
  // commentId: '12345678',
  // imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
  // numberOfLikes: 23,
  // isLiked: true,
  // user: { username: 'israel', userId: '23048394839403' },
  // content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
  // time: '20 minute ago'
  render() {
    const { comment } = this.props;
    return (
      <>
        <div className="card bg-dark text-light">
          <div className="card-header">
            {comment.user.username}
            <span className="text-muted h7 ml-3 mb-2">
              <i className="fa fa-clock-o" />
              {comment.time}
            </span>
          </div>
          <div className="card-body">
            <p>{comment.content}</p>
          </div>
          <div className="card-footer text-pink">
            <LikeButton onClick={() => this.props.onLiked(comment.commentId)} liked={comment.isLiked} />
            <span>{comment.numberOfLikes}</span>
          </div>
        </div>
        <hr />
      </>
    );
  }
}
