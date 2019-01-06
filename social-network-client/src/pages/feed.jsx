import React, { Component } from 'react';
import RouteProtector from '../HOC/routeProtector';
import Post from '../components/post';
class Feed extends Component {
  state = {
    posts: [
      {
        authorUsername: 'israel',
        authorFullname: 'israel kusayev',
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '20 minute ago'
      },
      {
        authorUsername: 'avi',
        authorFullname: 'avi aviov',
        content: ` Lorem ipsum dolor obcaecati hic, molestias minima iste voluptates.`,
        time: '50 minute ago'
      },
      {
        authorUsername: 'ruth',
        authorFullname: '-_-',
        content: ` Lorem ipsum dolor sit amet consectetur adipisicing elit. Quo recusandae nulla rem eos ipsa praesentium esse magnam nemo dolor sequi fuga quia quaerat cum, obcaecati hic, molestias minima iste voluptates.`,
        time: '55 minute ago'
      }
    ]
  };
  render() {
    return (
      <div>
        <h1>feed</h1>
        {this.state.posts.map((p, i) => {
          return <Post key={i} post={p} />;
        })}
      </div>
    );
  }
}

export default RouteProtector(Feed);
