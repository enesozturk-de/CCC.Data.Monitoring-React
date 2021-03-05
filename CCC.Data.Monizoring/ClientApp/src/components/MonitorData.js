import React, { Component } from 'react';

export class MonitorData extends Component {
    static displayName = MonitorData.name;

    constructor(props) {
        super(props);
        this.state = { monitordata: [], loading: true };
    }

    componentDidMount() {
        this.populateMonitorData();
        this.interval = setInterval(() => {
            this.populateMonitorData();
        }, 10000);
        
    }

    static renderMonitorDataTable(monitordatas) {
        return (
            <div class="table">
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Queue Group Name</th>
                        <th>Offered</th>
                        <th>Handled</th>
                        <th>Average Talk Time</th>
                        <th>Average Handling Time</th>
                        <th>Service Level</th> 
                    </tr>
                </thead>
                <tbody>
                    {monitordatas.map(monitordata =>
                        <tr key={monitordata.queueGroupName}>
                            <td>{monitordata.queueGroupName}</td>
                            <td>{monitordata.offered}</td>
                            <td>{monitordata.handled}</td>
                            <td>{monitordata.averageTalkTime}</td>
                            <td>{monitordata.averageHandlingTime}</td>
                            <td style={{ color: monitordata.columnColour, fontWeight: 600 }}>{monitordata.serviceLevel}</td> 
                        </tr>
                    )}
                </tbody>
                </table>
                </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Can`t Reach Whitout Logged In</em></p>
            : MonitorData.renderMonitorDataTable(this.state.monitordata);

        return (
            <div>
                <h1 id="tabelLabel" >Monitor Data</h1>
                <p>Monitoring Data Component for CCC.</p>
                {contents}
            </div>
        );
    }

    async populateMonitorData() {
        const response = await fetch('api/monitordata');
        const data = await response.json();
        this.setState({ monitordata: data, loading: false });
    }
}
