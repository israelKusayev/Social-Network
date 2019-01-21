import { Get } from './httpService';
import { getJwt } from "./jwtService";
import { toast } from 'react-toastify';
import * as signalR from '@aspnet/signalr';



const notificationUrl = process.env.REACT_APP_NOTIFICATIONS_URL + 'Notification';


let connection;
export async function Connect() {


  if (getJwt()) {

    const res = await GetNotifications();
    //  console.log(res);
    //  const data =await res.json();
    //  console.log(data[0]);
    //  const j =JSON.parse( data[0].dataJson);
    //  console.log(j);
    //  const { notifications } = this.state;
    //     notifications.push(j)
    //     this.setState({ notifications })

    connection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:44340/NotificationsHub', {
        accessTokenFactory: () => {
          return getJwt();
        }
      })
      .build();

    connection.onclose(() => toast.error("notification servicev disconnected..."))

    connection.on('getNotification', async (data) => {
      console.log(data);

      let notifications = []
      notifications.push(data)

    });

    connection
      .start()
      .then(() => console.log('connected to signalR ...'))
      .catch(() => console.log('connection faild...'));
  }
}

export async function GetNotifications() {
  return await Get(`${notificationUrl}/GetNotifications`, true);
}