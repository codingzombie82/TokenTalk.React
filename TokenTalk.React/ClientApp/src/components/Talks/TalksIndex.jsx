import React, { Component } from 'react';
import axios from 'axios';
export class TalksIndex extends Component {

    constructor(props) {
        super(props);

        this.state = {
            Talks: [],
            loading : true,
        };

    }
    componentDidMount() {
        this.populateTalkData();
    }



    renderTalksTable(talks) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Title</th>
                        <th>Description</th>
                        <th>Created</th>
                        <th>Action, Admin</th>
                    </tr>
                </thead>
                <tbody>
                    {talks.map(talk =>
                        <tr key={talk.id}>
                            <td>{talk.id}</td>
                            <td>{talk.title}</td>
                            <td>{talk.description}</td>
                            <td>{talk.created ? new Date(talk.created).toLocaleDateString() : "-"}</td>
                            <td className="text-nowrap">
                                <button className="btn btn-sm btn-primary" onClick={() => this.editBy(talk.id)}>Edit</button>
                                &nbsp;
                                <button className="btn btn-sm btn-danger" onClick={() => this.deleteBy(talk.id)}>Delete</button>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }
    createPage() {
        console.log('goCreatePage');
    }

    editBy(id) {
        console.log('editBy' + id);

    }

    deleteBy(id) {
        console.log('deleteBy' + id);

    }

    render() {

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderTalksTable(this.state.Talks);

        return (
            <div>
                <h1>Talk List &nbsp;
                    <button className="btn btn-primary" onClick={() => this.createPage()}>
                        <span className="fa fa-plus">+</span>
                    </button>
                </h1>

                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }
    // FETCH
    //async populateTalkData() {
    //    const response = await fetch('/api/Talks');
    //    const data = await response.json();
    //    this.setState({ Talks: data, loading: false });
    //}

    // AXIOS
    //populateTalkData() {
    //    axios.get('/api/Talks').then(response => {
    //        const data = response.data;
    //        this.setState({ Talks: data, loading: false });
    //    });
    //}

    // async AXIOS
    async populateTalkData() {
        const response = await axios.get('/api/Talks');
        const data = response.data;
        this.setState({ Talks: data, loading: false });
    }
}