import React, { Component } from 'react';
import RouteProtector from '../HOC/routeProtector';
import Post from '../components/post';
import InfiniteScroll from 'react-infinite-scroller';
import { getPosts } from '../services/postsService';
import { likePost, unlikePost } from '../services/likesService';

class Feed extends Component {
  pageSize = 5;

  state = {
    index: 0,
    reloadMorePosts: false,
    posts: []
  };

  componentDidMount = async () => {
    await this.getPosts();
  };

  getPosts = async () => {
    const res = await getPosts(this.state.index, this.pageSize);

    if (res.status === 200) {
      const data = await res.json();

      if (data && data.Posts && data.Posts.length !== 0) {
        let { posts, index } = this.state;
        index += this.pageSize;
        posts.push(...data.Posts);
        console.log(data);

        this.setState({ posts, index, reloadMorePosts: true });
      } else {
        this.setState({ reloadMorePosts: false });
      }
    }
  };

  onLiked = async (postId) => {
    const prevState = JSON.parse(JSON.stringify(this.state));

    const posts = [...this.state.posts];
    const post = posts.find((p) => p.postId === postId);
    post.isLiked = !post.isLiked;
    post.numberOfLikes = post.isLiked ? ++post.numberOfLikes : --post.numberOfLikes;
    this.setState({ posts });

    if (post.isLiked) {
      //like post
      const res = await likePost(post.postId);
      if (res.status !== 200) {
        this.setState({ ...prevState });
      }
    } else {
      //unlike post
      const res = await unlikePost(post.postId);
      if (res.status !== 200) {
        this.setState({ ...prevState });
      }
    }
  };

  render() {
    const { posts } = this.state;

    return (
      <div className="mt-3">
        <div className="row multipleLine">
          <div className="col-md-12">
            <InfiniteScroll
              pageStart={0}
              loadMore={this.getPosts}
              hasMore={this.state.reloadMorePosts}
              loader={
                <div key={0} className="spinner-border mx-auto text-dark" role="status">
                  <span className="sr-only">Loading...</span>
                </div>
              }
            >
              {posts.length !== 0 ? (
                posts.map((p) => {
                  return <Post onLiked={this.onLiked} key={p.postId} post={p} />;
                })
              ) : (
                <h2>No posts yet</h2>
              )}
            </InfiniteScroll>
          </div>
        </div>
      </div>
    );
  }
}

export default RouteProtector(Feed);
