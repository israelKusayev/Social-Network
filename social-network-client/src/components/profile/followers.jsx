import React, { Component } from 'react';
import FollowerTab from '../followerTab';

class Followers extends Component {
  state = {
    data: [
      { username: 'israel kusayev', following: true },
      { username: 'mosh', following: false },
      { username: 'avi aviov', following: true },
      { username: 'israel kusayev', following: true },
      { username: 'mosh', following: false },
      { username: 'avi aviov', following: true },
      { username: 'israel kusayev', following: true },
      { username: 'mosh', following: false },
      { username: 'avi aviov', following: true }
    ]
  };

  followBack = () => {};

  unfollow = () => {};

  blockUser = () => {};
  render() {
    const { data } = this.state;
    return (
      <div>
        <h1>Followers</h1>
        {data.map((user) => {
          return (
            <FollowerTab
              rightBtnName={'Block'}
              onRightBtnClicked={this.blockUser}
              leftBtnName={user.following ? 'unfollow' : 'Follow back'}
              onLeftBtnClicked={user.following ? this.unfollow : this.followBack}
              name={user.username}
            />
          );
        })}
      </div>
    );
  }
}

export default Followers;
