import { useEffect } from 'react';

const WebSocketComponent = ({ onActiveUsersUpdate, onTotalSalesUpdate, onTopProductsUpdate }) => {
    useEffect(() => {
        let ws;

        const connectWebSocket = () => {
            ws = new WebSocket('ws://localhost:5000/ws');

            ws.onopen = () => {
                console.log('Connected to WebSocket');
            };

            ws.onmessage = (message) => {
                const data = JSON.parse(message.data);
                console.log('Received data:', data);

                if (data.Count !== undefined) {
                    onActiveUsersUpdate(data);
                } else if (data.Amount !== undefined) {
                    onTotalSalesUpdate(data);
                } else if (Array.isArray(data)) {
                    onTopProductsUpdate(data);
                }
            };

            ws.onerror = (error) => {
                console.error('WebSocket error:', error);
            };

            ws.onclose = (event) => {
                console.log('Disconnected from WebSocket: ', event.code, event.reason);
                // Reconnect after 1 second
                setTimeout(connectWebSocket, 1000);
            };
        };

        connectWebSocket();

        return () => {
            if (ws && ws.readyState === WebSocket.OPEN) {
                ws.close();
            }
        };
    }, [onActiveUsersUpdate, onTotalSalesUpdate, onTopProductsUpdate]);

    return null;
};

export default WebSocketComponent;
