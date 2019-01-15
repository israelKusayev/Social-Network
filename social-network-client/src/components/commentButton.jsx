import React, { Component } from 'react';
import Comments from './comments';

export default class CommentButton extends Component {
  state = {
    commentsVisible: false
  };
  openComments = () => {
    this.setState({ commentsVisible: true });
  };
  closeComments = () => {
    this.setState({ commentsVisible: false });
  };

  render() {
    return (
      <>
        <span className="text-pink ">
          <span style={{ cursor: 'pointer' }} onClick={this.openComments}>
            <i className="fa fa-comments" />
            <span> Comment</span>
          </span>
          {this.state.commentsVisible && (
            <Comments postId={this.props.postId} isVisible={this.state.commentsVisible} onClose={this.closeComments} />
          )}
        </span>
      </>
    );
  }
}
