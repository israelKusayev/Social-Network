import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import LikeButton from './likeButton';
import moment from 'moment';
import { convertContent } from '../utils/referencing';

export default class Comment extends Component {
  render() {
    const { comment } = this.props;
    console.log(comment);

    const content = convertContent(comment.content, comment.referencing);
    return (
      <>
        <div className="card bg-dark text-light">
          <div className="card-header">
            <Link className="card-link" to={'/profile/' + comment.User.UserId}>
              <span className="h5 m-0 text-pink">@{comment.User.UserName}</span>
            </Link>
            <span className="text-muted h7 ml-3 mb-2">
              <i className="fa fa-clock-o" />
              {moment(comment.time).fromNow()}
            </span>
          </div>
          <div className="card-body">
            <p>{content}</p>
            <img src={comment.imgUrl} className="img-fluid" alt="" />
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
