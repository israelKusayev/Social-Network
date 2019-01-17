import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import signalR from '@aspnet/signalr';
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
        this.props.history.push('/profile/' + notification.user.userId);
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
      user: { userId: '34b4940f-9dfb-481e-a011-f16cb8ee8dd7', userName: 'israel' }
    },
    {
      actionId: 1,
      postId: 'd346fb1c-54d1-4e4f-99fe-6d9504a1a460',
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
      user: { userId: '34b4940f-9dfb-481e-a011-f16cb8ee8dd7', userName: 'shay' }
    }
  ];

  goToUserProfile = (e) => e.stopPropagation();

  componentDidMount = () => {
    let connection = new signalR.HubConnectionBuilder().withUrl('http://localhost:5000/notifications').build();

    connection.on('send', (data) => {
      console.log(data);
    });

    connection.start().then(() => connection.invoke('send', 'Hello'));

    // const nick = window.prompt('Your name:', 'John');

    // const hubConnection = new HubConnection('http://localhost:5000/chat');

    // this.setState({ hubConnection, nick }, () => {
    //   this.state.hubConnection
    //     .start()
    //     .then(() => console.log('Connection started!'))
    //     .catch((err) => console.log('Error while establishing connection :('));

    //   this.state.hubConnection.on('sendToAll', (nick, receivedMessage) => {
    //     const text = `${nick}: ${receivedMessage}`;
    //     const messages = this.state.messages.concat([text]);
    //     this.setState({ messages });
    //   });
    // });
  };

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
