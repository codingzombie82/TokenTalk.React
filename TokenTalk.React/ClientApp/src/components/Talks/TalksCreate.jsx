import React, { Component } from 'react';
import axios from 'axios';
export class TalksCreate extends Component {

    constructor(props) {
        super(props);

        this.state = {
            Talks: [],
            loading: true,
        };

    }
    componentDidMount() {
        
    }



    

    render() {

        return (
            <div>
                <h1>TalksCreate</h1>
            </div>
        );
    }
}