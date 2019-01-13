import React, { Component } from 'react';
import RouteProtector from '../HOC/routeProtector';
import Post from '../components/post';
import InfiniteScroll from 'react-infinite-scroller';
import { getPosts } from '../services/postsService';
import { likePost, unlikePost } from '../services/likesService';

class Feed extends Component {
  socialUrl = process.env.REACT_APP_SOCIAL_URL;
  componentDidMount = async () => {
    await this.getPosts();
  };
  pageSize = 5;

  state = {
    index: 0,
    reloadMorePosts: false,
    posts: [
      {
        postId: '12345678',
        imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
        numberOfLikes: 23,
        isLiked: true,
        User: { UserName: 'israel', UserId: '23048394839403' },
        content: ` Lorem ipsum .`,
        time: '20 minute ago'
      }
    ]
  };

  getPosts = async () => {
    const res = await getPosts(this.state.index, this.pageSize);
    if (res.status === 200) {
      const data = await res.json();
      let { posts, index } = this.state;
      index += this.pageSize;
      posts.push(...data.Posts);
      this.setState({ posts, index });
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
        console.log('like post faild, response - ', res);
      }
    } else {
      //unlike post
      const res = await unlikePost(post.postId);
      if (res.status !== 200) {
        console.log('unlike post faild, response - ', res);
      }
    }

    // if (!res || res.status !== 200) {
    //   this.setState({ ...prevState });
    // }
  };

  render() {
    return (
      <div className="mt-3">
        <div className="row">
          <div className="col-md-12">
            <InfiniteScroll
              pageStart={0}
              loadMore={this.getPosts}
              hasMore={this.state.reloadMorePosts}
              loader={
                <div className="spinner-border mx-auto text-dark" role="status">
                  <span className="sr-only">Loading...</span>
                </div>
              }
            >
              {this.state.posts.map((p, i) => {
                return <Post onLiked={this.onLiked} key={i} post={p} />;
              })}
            </InfiniteScroll>
          </div>
        </div>
      </div>
    );
  }
}

export default RouteProtector(Feed);
