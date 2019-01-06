import React, { Component } from 'react';
import FollowerTab from '../followerTab';

class Following extends Component {
  state = {
    data: [{ username: 'ruth' }, { username: 'david' }, { username: 'shir shir' }]
  };

  unfollow = () => {};

  blockUser = () => {};
  render() {
    const { data } = this.state;
    return (
      <div>
        <h1>Following</h1>
        {data.map((user) => {
          return (
            <FollowerTab
              rightBtnName={'Block'}
              onRightBtnClicked={this.blockUser}
              leftBtnName={'unfollow'}
              onLeftBtnClicked={this.unfollow}
              name={user.username}
            />
          );
        })}
      </div>
    );
  }
}

export default Following;
