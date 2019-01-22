import { Get } from './httpService';
import { getJwt } from './jwtService';
import { toast } from 'react-toastify';
import * as signalR from '@aspnet/signalr';

const notificationUrl = process.env.REACT_APP_NOTIFICATIONS_URL + 'Notification';

let notifications = [];
let connection = null;
let listener = null;
let unreadCountListener = null;

export function registerUnreadCount(func) {
  unreadCountListener = func;
}

export function register(func) {
  listener = func;
}

export function unregister() {
  listener = null;
}
export async function connect() {
  if (connection !== null) return;

  if (getJwt()) {
    const res = await await Get(`${notificationUrl}/GetNotifications`, true);
    const data = await res.json();
    data.forEach((element) => {
      notifications.unshift(JSON.parse(element));
    });
    if (listener) listener(notifications);

    connection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:44340/NotificationsHub', {
        accessTokenFactory: () => {
          return getJwt();
        }
      })
      .build();

    connection.onclose(() => toast.error('notification servicev disconnected...'));

    connection.on('getNotification', async (data) => {
      if (unreadCountListener) unreadCountListener();
      if (listener) listener(data);
      console.log(data);

      notifications.push(data);
    });

    connection
      .start()
      .then(() => console.log('connected to signalR ...'))
      .catch(() => console.log('connection faild...'));
  }
}

export function GetNotifications() {
  return [...notifications];
}
