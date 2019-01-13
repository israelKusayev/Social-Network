import React, { Component } from 'react';
import ProfileTemplate from './profileTemplate';
import { convertJsonToUser } from '../converters/userConvertor';
import User from '../models/user';
import { getUser, follow, Isfollow, unfollow, blockUser } from '../services/usersService';
import { toast } from 'react-toastify';

export default class UserProfile extends Component {
  state = {
    user: new User(),
    isFollow: false
  };

  componentDidMount = async () => {
    const res = await Isfollow(this.props.match.params.id);
    if (res.status === 200) {
      const isFollow = await res.json();
      this.setState({ isFollow });
      this.getUser();
    } else {
      this.props.history.goBack();
      toast.error('something went wrong...');
    }
  };

  getUser = async () => {
    const res = await getUser(this.props.match.params.id);

    if (res.status !== 200) {
      this.props.history.goBack();
      toast.error('something went wrong...');
    } else {
      var data = await res.json();
      let user = convertJsonToUser(data);
      this.setState({ user });
    }
  };

  ChangeFollowingState = async () => {
    if (this.state.isFollow) {
      const res = await unfollow(this.state.user.userId);
      if (res.status !== 200) {
        toast.error('faild to unfollow, try again.');
      } else {
        this.setState({ isFollow: false });
      }
    } else {
      const res = await follow(this.state.user.userId);
      if (res.status !== 200) {
        toast.error('faild to follow, try again.');
      } else {
        this.setState({ isFollow: true });
      }
    }
  };

  blockUser = async () => {
    const res = await blockUser(this.props.match.params.id);

    if (res.status !== 200) {
      toast.error('something went wrong...');
    } else {
      this.props.history.replace('/');
      toast.success('User has been blocked successfully');
    }
  };

  render() {
    const { user } = this.state;

    return (
      <>
        <div className="my-3">
          <div className="row">
            <button onClick={this.ChangeFollowingState} className="offset-5 btn btn-dark text-pink">
              {this.state.isFollow ? 'unfollow' : 'follow'}
            </button>
            <button onClick={this.blockUser} className="offset-1 btn btn-dark text-pink">
              block
            </button>
          </div>
        </div>
        <ProfileTemplate title={'User details'} isReadOnly={true} user={user} />
      </>
    );
  }
}
