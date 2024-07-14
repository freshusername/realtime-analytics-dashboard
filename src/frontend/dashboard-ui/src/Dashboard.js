import React, { useState } from 'react';
import { Line, Bar } from 'react-chartjs-2';
import 'chart.js/auto';
import WebSocketComponent from './WebSocketComponent';
import './App.css';

const Dashboard = () => {
    const [activeUsers, setActiveUsers] = useState([]);
    const [totalSales, setTotalSales] = useState([]);
    const [topProducts, setTopProducts] = useState([]);
    const [lastUpdateActiveUsers, setLastUpdateActiveUsers] = useState(null);
    const [lastUpdateTotalSales, setLastUpdateTotalSales] = useState(null);
    const [lastUpdateTopProducts, setLastUpdateTopProducts] = useState(null);

    const handleActiveUsersUpdate = (data) => {
        setActiveUsers(prevState => {
            const newState = prevState.filter(item => item.Timestamp !== data.Timestamp);
            newState.push(data);
            return newState;
        });
        setLastUpdateActiveUsers(new Date(data.Timestamp));
    };

    const handleTotalSalesUpdate = (data) => {
        setTotalSales(prevState => {
            const newState = prevState.filter(item => item.Timestamp !== data.Timestamp);
            newState.push(data);
            return newState;
        });
        setLastUpdateTotalSales(new Date(data.Timestamp));
    };

    const handleTopProductsUpdate = (data) => {
        setTopProducts(data);
        setLastUpdateTopProducts(new Date());
    };

    const formatTimestamp = (timestamp) => {
        const date = new Date(timestamp);
        return date.toLocaleTimeString('en-US', {
            hour: '2-digit',
            minute: '2-digit',
            second: '2-digit'
        });
    };

    const renderLastUpdate = (lastUpdate) => {
        return lastUpdate ? `Last updated at ${formatTimestamp(lastUpdate)}` : 'Waiting for data...';
    };

    const activeUsersData = {
        labels: activeUsers.map(user => formatTimestamp(user.Timestamp)),
        datasets: [
            {
                label: 'Active Users',
                data: activeUsers.map(user => user.Count),
                borderColor: 'rgba(75,192,192,1)',
                backgroundColor: 'rgba(75,192,192,0.2)',
            },
        ],
    };

    const totalSalesData = {
        labels: totalSales.map(sales => formatTimestamp(sales.Timestamp)),
        datasets: [
            {
                label: 'Total Sales',
                data: totalSales.map(sales => sales.Amount),
                borderColor: 'rgba(153,102,255,1)',
                backgroundColor: 'rgba(153,102,255,0.2)',
            },
        ],
    };

    const topProductsData = {
        labels: topProducts.map((product) => product.Name),
        datasets: [
            {
                label: 'Top Selling Products',
                data: topProducts.map((product) => product.QuantitySold),
                borderColor: 'rgba(255,159,64,1)',
                backgroundColor: 'rgba(255,159,64,0.2)',
            },
        ],
    };

    return (
        <div className="dashboard">
            <h1>Real-Time Analytics Dashboard</h1>
            <div className="charts-container">
                <div className="chart-card">
                    <h2>Active Users</h2>
                    <p>Updates every 10 seconds</p>
                    <p>{renderLastUpdate(lastUpdateActiveUsers)}</p>
                    {activeUsers.length > 0 ? (
                        <Line data={activeUsersData} />
                    ) : (
                        <p>Loading...</p>
                    )}
                </div>
                <div className="chart-card">
                    <h2>Total Sales</h2>
                    <p>Updates every 10 seconds</p>
                    <p>{renderLastUpdate(lastUpdateTotalSales)}</p>
                    {totalSales.length > 0 ? (
                        <Line data={totalSalesData} />
                    ) : (
                        <p>Loading...</p>
                    )}
                </div>
                <div className="chart-card">
                    <h2>Top Selling Products</h2>
                    <p>Updates every 30 seconds</p>
                    <p>{renderLastUpdate(lastUpdateTopProducts)}</p>
                    {topProducts.length > 0 ? (
                        <Bar data={topProductsData} />
                    ) : (
                        <p>Loading...</p>
                    )}
                </div>
            </div>
            <WebSocketComponent
                onActiveUsersUpdate={handleActiveUsersUpdate}
                onTotalSalesUpdate={handleTotalSalesUpdate}
                onTopProductsUpdate={handleTopProductsUpdate}
            />
        </div>
    );
};

export default Dashboard;
