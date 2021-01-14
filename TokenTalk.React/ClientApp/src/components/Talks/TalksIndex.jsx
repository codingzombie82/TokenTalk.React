import React, { Component } from 'react';

export class TalksIndex extends Component {

    constructor(props) {
        super(props);

        this.state = {
            Talks: [],
            loading : true,
        };
    }
    componentDidMount() {
        this.populateWeatherData();
    }

    static renderTalksTable(talks) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Title</th>
                        <th>-</th>
                        <th>-</th>
                    </tr>
                </thead>
                <tbody>
                    {talks.map(talks =>
                        <tr key={talks.id}>
                            <td>{talks.id}</td>
                            <td>{talks.title}</td>
                            <td>{talks.title}</td>
                            <td>{talks.title}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : TalksIndex.renderTalksTable(this.state.Talks);

        return (
            <div>
                <h1>Talk List</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateWeatherData() {
        const response = await fetch('/api/Talks');
        const data = await response.json();
        console.log(data);
        this.setState({ Talks: data, loading: false });
    }
}