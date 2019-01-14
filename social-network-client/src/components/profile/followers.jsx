import React, { Component } from 'react';
import FollowerTab from '../followerTab';
import { getFollowers, unfollow, follow, blockUser } from '../../services/usersService';
import { toast } from 'react-toastify';
import { removeItemFromArray } from '../../utils/removeFromArray';

class Followers extends Component {
  state = {
    followers: []
  };

  componentDidMount = async () => {
    const res = await getFollowers();
    if (res.status !== 200) {
      toast.error('something went wrong...');
    } else {
      const followers = await res.json();
      this.setState({ followers });
    }
  };

  followBack = async (user) => {
    const res = await follow(user.userId);
    if (res.status !== 200) {
      toast.error('faild to follow, try again.');
    } else {
      this.changeFollowState(user);
    }
  };

  unfollow = async (user) => {
    const res = await unfollow(user.userId);
    if (res.status !== 200) {
      toast.error('faild to unfollow, try again.');
    } else {
      this.changeFollowState(user);
    }
  };

  changeFollowState = (user) => {
    const followers = [...this.state.followers];
    const index = followers.indexOf(user);
    followers[index].isFollowing = !followers[index].isFollowing;
    this.setState({ followers });
  };

  blockUser = async (user) => {
    const res = await blockUser(user.userId);

    if (res.status !== 200) {
      toast.error('something went wrong...');
    } else {
      const followers = removeItemFromArray([...this.state.followers], user);
      this.setState({ followers });
    }
  };
  render() {
    const { followers } = this.state;
    console.log(followers);

    return (
      <div>
        <h1>Followers</h1>
        {followers.length !== 0 ? (
          followers.map((user) => {
            return (
              <FollowerTab
                key={user.userId}
                rightBtnName={'Block'}
                onRightBtnClicked={() => this.blockUser(user)}
                leftBtnName={user.isFollowing ? 'unfollow' : 'Follow back'}
                onLeftBtnClicked={user.isFollowing ? () => this.unfollow(user) : () => this.followBack(user)}
                name={user.username}
              />
            );
          })
        ) : (
          <h2 className="text-danger">No one is following you</h2>
        )}
      </div>
    );
  }
}

export default Followers;
