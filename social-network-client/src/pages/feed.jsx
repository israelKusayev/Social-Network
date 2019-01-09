import React, { Component } from 'react';
import RouteProtector from '../HOC/routeProtector';
import Post from '../components/post';
import { Get, Put } from '../services/httpService';
import InfiniteScroll from 'react-infinite-scroller';

class Feed extends Component {
  socialUrl = process.env.REACT_APP_SOCIAL_URL;
  componentDidMount = () => {
    this.setState({ reloadMorePosts: true });
    this.getPosts();
  };
  pageSize = 5;

  state = {
    index: 0,
    reloadMorePosts: false,
    posts: [],
    fakeData: [
      {
        postId: '12345678',
        imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
        numberOfLikes: 23,
        isLiked: true,
        user: { username: 'israel', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '20 minute ago'
      },
      {
        postId: '12345dfdf678',

        numberOfLikes: 23,
        isLiked: true,
        user: { username: 'mosh', userId: 'dfdfddfdfdfd' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit.1`,
        time: '20 minute ago'
      },
      {
        postId: 'fdfdsfdfdfd',

        numberOfLikes: 13,
        isLiked: false,
        user: { username: 'avi', userId: '2304839d4839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '20 minute ago'
      },
      {
        postId: '123s45678',
        numberOfLikes: 3,
        isLiked: true,
        user: { username: 'israel', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '23 minute ago'
      },
      {
        postId: '1234d5678',
        imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
        numberOfLikes: 11,
        isLiked: false,
        user: { username: 'ruth', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, `,
        time: '10 minute ago'
      },
      {
        postId: '12345678',
        imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
        numberOfLikes: 23,
        isLiked: true,
        user: { username: 'israel', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '20 minute ago'
      },
      {
        postId: '12345dfdf678',

        numberOfLikes: 23,
        isLiked: true,
        user: { username: 'mosh', userId: 'dfdfddfdfdfd' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit.1`,
        time: '20 minute ago'
      },
      {
        postId: 'fdfdsfdfdfd',

        numberOfLikes: 13,
        isLiked: false,
        user: { username: 'avi', userId: '2304839d4839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '20 minute ago'
      },
      {
        postId: '123s45678',
        numberOfLikes: 3,
        isLiked: true,
        user: { username: 'israel', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '23 minute ago'
      },
      {
        postId: '1234d5678',
        imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
        numberOfLikes: 11,
        isLiked: false,
        user: { username: 'ruth', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, `,
        time: '10 minute ago'
      },
      {
        postId: '12345678',
        imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
        numberOfLikes: 23,
        isLiked: true,
        user: { username: 'israel', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '20 minute ago'
      },
      {
        postId: '12345dfdf678',

        numberOfLikes: 23,
        isLiked: true,
        user: { username: 'mosh', userId: 'dfdfddfdfdfd' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit.1`,
        time: '20 minute ago'
      },
      {
        postId: 'fdfdsfdfdfd',

        numberOfLikes: 13,
        isLiked: false,
        user: { username: 'avi', userId: '2304839d4839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '20 minute ago'
      },
      {
        postId: '123s45678',
        numberOfLikes: 3,
        isLiked: true,
        user: { username: 'israel', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '23 minute ago'
      },
      {
        postId: '1234d5678',
        imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
        numberOfLikes: 11,
        isLiked: false,
        user: { username: 'ruth', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, `,
        time: '10 minute ago'
      },
      {
        postId: '12345678',
        imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
        numberOfLikes: 23,
        isLiked: true,
        user: { username: 'israel', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '20 minute ago'
      },
      {
        postId: '12345dfdf678',

        numberOfLikes: 23,
        isLiked: true,
        user: { username: 'mosh', userId: 'dfdfddfdfdfd' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit.1`,
        time: '20 minute ago'
      },
      {
        postId: 'fdfdsfdfdfd',

        numberOfLikes: 13,
        isLiked: false,
        user: { username: 'avi', userId: '2304839d4839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '20 minute ago'
      },
      {
        postId: '123s45678',
        numberOfLikes: 3,
        isLiked: true,
        user: { username: 'israel', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '23 minute ago'
      },
      {
        postId: '1234d5678',
        imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
        numberOfLikes: 11,
        isLiked: false,
        user: { username: 'ruth', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, `,
        time: '10 minute ago'
      },
      {
        postId: '12345678',
        imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
        numberOfLikes: 23,
        isLiked: true,
        user: { username: 'israel', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '20 minute ago'
      },
      {
        postId: '12345dfdf678',

        numberOfLikes: 23,
        isLiked: true,
        user: { username: 'mosh', userId: 'dfdfddfdfdfd' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit.1`,
        time: '20 minute ago'
      },
      {
        postId: 'fdfdsfdfdfd',

        numberOfLikes: 13,
        isLiked: false,
        user: { username: 'avi', userId: '2304839d4839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '20 minute ago'
      },
      {
        postId: '123s45678',
        numberOfLikes: 3,
        isLiked: true,
        user: { username: 'israel', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '23 minute ago'
      },
      {
        postId: '1234d5678',
        imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
        numberOfLikes: 11,
        isLiked: false,
        user: { username: 'ruth', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, `,
        time: '10 minute ago'
      },
      {
        postId: '12345678',
        imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
        numberOfLikes: 23,
        isLiked: true,
        user: { username: 'israel', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '20 minute ago'
      },
      {
        postId: '12345dfdf678',

        numberOfLikes: 23,
        isLiked: true,
        user: { username: 'mosh', userId: 'dfdfddfdfdfd' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit.1`,
        time: '20 minute ago'
      },
      {
        postId: 'fdfdsfdfdfd',

        numberOfLikes: 13,
        isLiked: false,
        user: { username: 'avi', userId: '2304839d4839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '20 minute ago'
      },
      {
        postId: '123s45678',
        numberOfLikes: 3,
        isLiked: true,
        user: { username: 'israel', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '23 minute ago'
      },
      {
        postId: '1234d5678',
        imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
        numberOfLikes: 11,
        isLiked: false,
        user: { username: 'ruth', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, `,
        time: '10 minute ago'
      },
      {
        postId: '12345678',
        imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
        numberOfLikes: 23,
        isLiked: true,
        user: { username: 'israel', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '20 minute ago'
      },
      {
        postId: '12345dfdf678',

        numberOfLikes: 23,
        isLiked: true,
        user: { username: 'mosh', userId: 'dfdfddfdfdfd' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit.1`,
        time: '20 minute ago'
      },
      {
        postId: 'fdfdsfdfdfd',

        numberOfLikes: 13,
        isLiked: false,
        user: { username: 'avi', userId: '2304839d4839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '20 minute ago'
      },
      {
        postId: '123s45678',
        numberOfLikes: 3,
        isLiked: true,
        user: { username: 'israel', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '23 minute ago'
      },
      {
        postId: '1234d5678',
        imgUrl: `data:image/gif;base64,R0lGODlhEAAQAMQAAORHHOVSKudfOulrSOp3WOyDZu6QdvCchPGolfO0o/XBs/fNwfjZ0frl3/zy7////wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAkAABAALAAAAAAQABAAAAVVICSOZGlCQAosJ6mu7fiyZeKqNKToQGDsM8hBADgUXoGAiqhSvp5QAnQKGIgUhwFUYLCVDFCrKUE1lBavAViFIDlTImbKC5Gm2hB0SlBCBMQiB0UjIQA7`,
        numberOfLikes: 11,
        isLiked: false,
        user: { username: 'ruth', userId: '23048394839403' },
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, `,
        time: '10 minute ago'
      }
    ]
  };

  // getPosts = async () => {
  //   const res = await Get(`${this.socialUrl}getPosts/${this.state.index}/${this.pageSize + 10}`);
  //   if (res.status === 200) {
  //     const data = res.json();

  //     let { posts, index } = this.state;
  //     index += this.pageSize;
  //     posts.push(...data);
  //     this.setState({ posts, index });
  //   }
  // };

  getPosts = async () => {
    let { posts, index, fakeData } = this.state;
    if (index > 18) this.setState({ reloadMorePosts: false });

    const data = fakeData.slice(index, index + this.pageSize);
    index += this.pageSize;
    posts.push(...data);
    this.setState({ posts, index });
  };

  onLiked = async (postId) => {
    const prevState = JSON.parse(JSON.stringify(this.state));

    const posts = [...this.state.posts];
    const post = posts.find((p) => p.postId === postId);
    post.isLiked = !post.isLiked;
    post.numberOfLikes = post.isLiked ? ++post.numberOfLikes : --post.numberOfLikes;
    this.setState({ posts });

    const res = await Put(`${this.socialUrl}/likePost/${post.postId}/${post.user.userId}`);

    if (!res || res.status !== 200) {
      this.setState({ ...prevState });
    }
  };

  render() {
    return (
      <div className="mt-3">
        <div className="row">
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
    );
  }
}

export default RouteProtector(Feed);
