import React, { Component } from 'react';
import FollowerTab from '../followerTab';
import { getFollowings, blockUser, unfollow } from '../../services/usersService';
import { toast } from 'react-toastify';
import { removeItemFromArray } from '../../utils/removeFromArray';

class Following extends Component {
  state = {
    followings: []
  };
  componentDidMount = async () => {
    const res = await getFollowings();
    if (res.status === 200) {
      const followings = await res.json();
      this.setState({ followings });
    } else {
      toast.error('something went wrong...');
    }
  };

  unfollow = async (user) => {
    const res = await unfollow(user.UserId);
    if (res.status !== 200) {
      toast.error('faild to unfollow, try again.');
    } else {
      const followings = removeItemFromArray([...this.state.followings], user);
      this.setState({ followings });
    }
  };

  blockUser = async (user) => {
    const res = await blockUser(user.UserId);

    if (res.status !== 200) {
      toast.error('something went wrong...');
    } else {
      const followings = removeItemFromArray([...this.state.followings], user);
      this.setState({ followings });
    }
  };

  render() {
    const { followings } = this.state;
    return (
      <div>
        <h1>Following</h1>

        {followings.length !== 0 ? (
          followings.map((user) => {
            return (
              <FollowerTab
                key={user.UserId}
                rightBtnName={'Block'}
                onRightBtnClicked={() => this.blockUser(user)}
                leftBtnName={'unfollow'}
                onLeftBtnClicked={() => this.unfollow(user)}
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
