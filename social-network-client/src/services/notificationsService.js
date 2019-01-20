import { Get } from './httpService';

const notificationUrl = process.env.REACT_APP_NOTIFICATIONS_URL + 'Notification';

export async function GetNotifications(userId) {
  return await Get(`${notificationUrl}/GetNotifications/${userId}`, true);
}