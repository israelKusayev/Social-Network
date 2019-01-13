import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import LikeButton from './likeButton';
import CommentButton from './commentButton';

export default class Post extends Component {
  // {
  //   postId: '12345678',
  //   imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
  //   numberOfLikes: 23,
  //   isLiked: true,
  //   user: { username: 'israel', userId: '23048394839403' },
  //   content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
  //   time: '20 minute ago'
  // },
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
                    <img
                      className="rounded-circle"
                      width="45"
                      src="data:image/png;base64,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"
                      alt=""
                    />
                  </div>
                  <div className="ml-2">
                    <Link className="card-link" to={'/profile/' + post.User.UserId}>
                      <div className="h5 m-0 text-pink">@{post.User.UserName}</div>
                    </Link>
                    {/* <div className="h7 text-muted">{post.authorFullname}</div> */}
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
              <img src={post.imgUrl} className="img-fluid" alt="" />
            </div>
            <div className="card-footer text-pink ">
              <LikeButton liked={post.isLiked} onClick={() => this.props.onLiked(post.postId)} />
              <span>{post.numberOfLikes}</span>
              <span className="mx-2" />
              <CommentButton postId={post.postId} />
            </div>
          </div>
        </div>
      </div>
    );
  }
}
