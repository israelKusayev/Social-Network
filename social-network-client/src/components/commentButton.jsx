import React, { Component } from 'react';
import CommentPopup from './commentsPopup';

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
        <span style={{ cursor: 'pointer' }} onClick={this.openComments}>
          <i className="fa fa-comments" />
          <span className="text-pink"> Comment</span>
        </span>
        <CommentPopup isVisible={this.state.commentsVisible} onClose={this.closeComments} />
      </>
    );
  }
}
