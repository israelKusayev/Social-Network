import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import LikeButton from './likeButton';
import CommentButton from './commentButton';

export default class Post extends Component {
  state = {
    liked: false
  };
  onLiked = () => {
    this.setState({ liked: !this.state.liked });
  };
  render() {
    const { post } = this.props;
    return (
      <div className="row mb-4">
        <div className="col-md-8 offset-2">
          <div className="card text-white bg-dark">
            <div className="card-header">
              <div className="d-flex justify-content-between align-items-center">
                <div className="d-flex justify-content-between align-items-center">
                  <div className="mr-2">
                    <img className="rounded-circle" width="45" src="https://picsum.photos/50/50" alt="" />
                  </div>
                  <div className="ml-2">
                    <Link className="card-link" to={'/profile/' + post.authorUsername}>
                      <div className="h5 m-0 text-pink">@{post.authorUsername}</div>
                    </Link>
                    <div className="h7 text-muted">{post.authorFullname}</div>
                  </div>
                </div>
              </div>
            </div>
            <div className="card-body">
              <div className="text-muted h7 mb-2">
                <i className="fa fa-clock-o" />
                {post.time}
              </div>

              <p className="card-text">{post.content}</p>
            </div>
            <div className="card-footer text-pink ">
              <LikeButton liked={this.state.liked} onClick={this.onLiked} />
              <span className="mx-1" />
              <CommentButton />
            </div>
          </div>
        </div>
      </div>
    );
  }
}
