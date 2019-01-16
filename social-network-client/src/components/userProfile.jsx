import React, { Component } from 'react';
import ProfileTemplate from './profileTemplate';
import { convertJsonToUser } from '../converters/userConvertor';
import User from '../models/user';
import { getUser, follow, Isfollow, unfollow, blockUser } from '../services/usersService';
import { toast } from 'react-toastify';
import { getUserId } from '../services/jwtService';

export default class UserProfile extends Component {
  state = {
    user: new User(),
    isFollow: false
  };

  componentDidMount = async () => {
    if (this.props.match.params.id === getUserId()) {
      this.props.history.replace('/profile');
    }
    const userId = this.props.match.params.id;
    this.setState({ isFollow: true });
    await this.getUser(userId);
    await this.isFollow(userId);
  };

  componentWillReceiveProps = async (nextProps) => {
    const userId = nextProps.match.params.id;

    if (userId !== this.props.match.params.id) {
      await this.getUser(userId);
      await this.isFollow(userId);
    }
  };

  isFollow = async (userId) => {
    const res = await Isfollow(userId);
    if (res.status === 200) {
      const isFollow = await res.json();
      this.setState({ isFollow });
    } else {
      this.props.history.goBack();
      toast.error('something went wrong...');
    }
  };

  getUser = async (userId) => {
    const res = await getUser(userId);
    if (res.status === 200) {
      let data = await res.json();
      let user = convertJsonToUser(data);
      this.setState({ user });
    } else if (res.status === 400) {
      let data = await res.json();
      console.log('400', data);
      this.props.history.goBack();
      toast.error(data.Message);
    } else {
      this.props.history.goBack();
      toast.error('something went wrong...');
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
