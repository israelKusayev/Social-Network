import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import RouteProtector from '../HOC/routeProtector';

class Notifications extends Component {
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
      case 6:
        {
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

  goToUserProfile = (e) => e.stopPropagation();

  render() {
    console.log(this.props)
    return (
      <div>
        <h1>Notifications</h1>
        <div className="row">
          <div className="col-md-8 offset-2">
            {this.props.notifications.map((n,i) => {
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
