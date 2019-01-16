import { client, xml } from '@xmpp/client';

const xmpp = client({
  service: 'http://localhost:5280/admin/',
  domain: 'DESKTOP-NG5P9OM',
  resource: 'example',
  username: 'admin',
  password: 'admin'
});

xmpp.on('error', (err) => {
  console.error('❌', err.toString());
});

xmpp.on('offline', () => {
  console.log('⏹', 'offline');
});

xmpp.on('stanza', async (stanza) => {
  if (stanza.is('message')) {
    await xmpp.send(xml('presence', { type: 'unavailable' }));
    await xmpp.stop();
  }
});

xmpp.on('online', async (address) => {
  console.log('▶', 'online as', address.toString());

  // Makes itself available
  await xmpp.send(xml('presence'));

  // Sends a chat message to itself
  const message = xml('message', { type: 'chat', to: address }, xml('body', 'hello world'));
  await xmpp.send(message);
});

// Debug
xmpp.on('status', (status) => {
  console.debug('🛈', 'status', status);
});
xmpp.on('input', (input) => {
  console.debug('⮈', input);
});
xmpp.on('output', (output) => {
  console.debug('⮊', output);
});

export default xmpp.start().catch(console.error);
