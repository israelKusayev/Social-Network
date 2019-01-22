import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import RouteProtector from '../HOC/routeProtector';
import { register, unregister, GetNotifications } from '../services/notificationsService';

class Notifications extends Component {
  state = {
    notifications: []
  };

  componentDidMount() {
    register(this.whenNotified);
    const notifications = GetNotifications();
    this.setState({ notifications });
  }

  handleNotificationClick = (notification) => {
    switch (notification.actionId) {
      case 0:
      case 2:
      case 3: {
        this.props.history.push('/post/' + notification.postId);
        break;
      }
      case 1:
      case 4:
      case 6: {
        this.props.history.push('/comment/' + notification.postId);
        break;
      }
      case 5: {
        this.props.history.push('/profile/' + notification.user.userId);
        break;
      }

      default:
        break;
    }
  };

  action = [
    'like your post',
    'like your comment',
    'commented on your post',
    'Mentioned you in post',
    'Mentioned you in comment',
    'followed you'
  ];

  whenNotified = (data) => {
    console.log(data);

    const { notifications } = this.state;
    if (Array.isArray(data)) {
      console.log('is array');

      console.log(notifications);

      notifications.unshift(...data);

      console.log(notifications);
    } else {
      console.log('is singal');

      console.log(notifications);

      notifications.unshift(data);
      console.log(notifications);
    }

    this.setState({ notifications });
  };

  componentWillUnmount = () => {
    unregister();
  };

  goToUserProfile = (e) => e.stopPropagation();

  render() {
    if (this.state.notifications.length === 0) return <h2 className="text-danger">No notifications yet</h2>;
    return (
      <div>
        <div className="row mt-4">
          <div className="col-md-8 offset-2">
            {this.state.notifications.map((n, i) => {
              return (
                <div
                  className="alert alert-dark app-bg text-white cursor-p text-center font-size-bigger"
                  key={i}
                  onClick={() => this.handleNotificationClick(n)}
                >
                  <Link onClick={this.goToUserProfile} className="text-pink bold" to={'/profile/' + n.user.userId}>
                    {n.user.userName}
                  </Link>
                  {' ' + this.action[n.actionId]}
                </div>
              );
            })}
          </div>
        </div>
      </div>
    );
  }
}

export default RouteProtector(Notifications);
