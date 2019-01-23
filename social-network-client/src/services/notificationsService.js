import { Get } from './httpService';
import { getJwt } from './jwtService';
import * as signalR from '@aspnet/signalr';

const notificationUrl =
    process.env.REACT_APP_NOTIFICATIONS_URL + 'Notification';

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
        const res = await Get(`${notificationUrl}/GetNotifications`, true);
        if (res.status === 200) {
            const data = await res.json();
            data.forEach((element) => {
                notifications.unshift(JSON.parse(element));
            });
            if (listener) listener(notifications);
        }

        connection = new signalR.HubConnectionBuilder()
            .withUrl(process.env.REACT_APP_SIGNALR_URL, {
                accessTokenFactory: () => {
                    return getJwt();
                }
            })
            .build();

        connection.on('getNotification', async (data) => {
            if (unreadCountListener) unreadCountListener();
            if (listener) listener(data);
            notifications.unshift(data);
        });

        connection
            .start()
            .then(() => console.log('connected to signalR ...'))
            .catch(() => console.log('connection faild...'));
    }
}

export async function closeConnection() {
    notifications = [];
    await connection.stop();
    connection = null;
}

export function GetNotifications() {
    return [...notifications];
}
