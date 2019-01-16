import React, { Component } from 'react';
import PostComponent from '../components/post';
import { getPost } from '../services/postsService';
import { toast } from 'react-toastify';
import Spinner from '../components/spinner';
import { likePost, unlikePost } from '../services/likesService';
export default class Post extends Component {
  state = { post: null };

  componentDidMount = async () => {
    const res = await getPost(this.props.match.params.postId);
    if (res.status !== 200) {
      toast.error('something faild...');
    } else {
      const post = await res.json();
      this.setState({ post });
    }
  };

  onLiked = async (postId) => {
    const prevPost = { ...this.state.post };

    const post = this.state.post;
    post.isLiked = !post.isLiked;
    post.numberOfLikes = post.isLiked ? ++post.numberOfLikes : --post.numberOfLikes;
    this.setState({ post });

    if (post.isLiked) {
      //like post
      const res = await likePost(post.postId);
      if (res.status !== 200) {
        this.setState({ post: prevPost });
      }
    } else {
      //unlike post
      const res = await unlikePost(post.postId);
      if (res.status !== 200) {
        this.setState({ post: prevPost });
      }
    }
  };

  render() {
    const { post } = this.state;

    return (
      <>
        {post ? (
          <div className="mt-5">
            <PostComponent onLiked={this.onLiked} key={post.postId} post={post} />{' '}
          </div>
        ) : (
          <Spinner />
        )}
      </>
    );
  }
}
