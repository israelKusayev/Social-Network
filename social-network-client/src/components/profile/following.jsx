import React, { Component } from 'react';
import FollowerTab from '../followerTab';
import { getFollowings } from '../../services/usersService';

class Following extends Component {
  state = {
    followings: []
  };
  componentDidMount = async () => {
    const res = await getFollowings();
    if (res.status === 200) {
      const followings = await res.json();
      this.setState({ followings });
      console.log(followings);
    } else {
      console.log('get following faild.');
    }
  };

  unfollow = (userId) => {
    console.log(userId);
  };

  blockUser = () => {};
  render() {
    const { followings } = this.state;
    return (
      <div>
        <h1>Following</h1>

        {followings.length !== 0 ? (
          followings.map((user) => {
            return (
              <FollowerTab
                rightBtnName={'Block'}
                onRightBtnClicked={this.blockUser}
                leftBtnName={'unfollow'}
                onLeftBtnClicked={() => this.unfollow(user.UserId)}
                name={user.UserName}
              />
            );
          })
        ) : (
          <h2 className="text-danger">You don't follow anyone, yet</h2>
        )}
      </div>
    );
  }
}

export default Following;
