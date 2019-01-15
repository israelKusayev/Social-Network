import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import RouteProtector from '../HOC/routeProtector';

class Notifications extends Component {
  handleNotificationClick = (notification) => {
    console.log(notification.actionId);

    switch (notification.actionId) {
      case 0:
      case 2:
      case 3: {
        this.props.history.push('/post/' + notification.postId);
        break;
      }
      case 1 || 4: {
        this.props.history.push('/comment/' + notification.postId);
        break;
      }
      case 5: {
        this.props.history.push('/profile/' + notification.postId);
        break;
      }
      case 6: {
        this.props.history.push('/comment/' + notification.postId);
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
  Notifications = [
    {
      actionId: 0,
      postId: 'd346fb1c-54d1-4e4f-99fe-6d9504a1a460',
      user: { userId: '1234', userName: 'israel' }
    },
    {
      actionId: 1,
      commentId: '12344',
      user: { userId: '12343', userName: 'omer' }
    },
    {
      actionId: 2,
      postId: '123433',
      user: { userId: '13234', userName: 'shahar' }
    },
    {
      actionId: 3,
      postId: '123143',
      user: { userId: '12534', userName: 'israel' }
    },
    {
      actionId: 4,
      commentId: '12345',
      user: { userId: '12345', userName: 'shay' }
    },
    {
      actionId: 5,
      user: { userId: '128345', userName: 'shay' }
    }
  ];
  render() {
    return (
      <div>
        <h1>Notifications</h1>
        <div className="row">
          <div className="col-md-8 offset-2">
            {this.Notifications.map((n) => {
              return (
                <div
                  className="alert alert-dark app-bg text-white cursor-p text-center font-size-bigger"
                  key={n.user.userId}
                  onClick={() => this.handleNotificationClick(n)}
                >
                  <Link className="text-pink bold" to={'/profile/' + n.user.userId}>
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
