import React, { Component } from 'react';
import LikeButton from './likeButton';
import moment from 'moment';

export default class Comment extends Component {
  render() {
    const { comment } = this.props;
    return (
      <>
        <div className="card bg-dark text-light">
          <div className="card-header">
            {comment.User.UserName}
            <span className="text-muted h7 ml-3 mb-2">
              <i className="fa fa-clock-o" />
              {moment(comment.time).fromNow()}
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
