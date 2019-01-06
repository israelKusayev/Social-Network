import React from 'react';
import Modal from 'react-bootstrap4-modal';
import Comment from './comment';

export default class CommentPopup extends React.Component {
  // event handling methods go here

  render() {
    return (
      <Modal dialogClassName="modal-lg " visible={this.props.isVisible} onClickBackdrop={this.modalBackdropClicked}>
        <div className="bg-dark text-white">
          <div className="modal-header">
            <h5 className="modal-title">Comments</h5>
            <button className="btn">
              <i className="fa fa-times text-pink" onClick={this.props.onClose} />
            </button>
          </div>
          <div className="modal-body ">
            <div className="card bg-dark mb-4">
              <div className="card-body">
                <form>
                  <h5>Add comment</h5>
                  <div className="form-group">
                    <textarea type="text" className="form-control" placeholder="content" />
                  </div>
                  <button type="submit" className="btn btn-pink">
                    Add
                  </button>
                </form>
              </div>
            </div>

            <Comment />
            <Comment />
            <Comment />
            <Comment />
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
