﻿import React, { Component } from 'react';
import axios from 'axios';
export class TalksEdit extends Component {

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
                <h1>TalksEdit</h1>
            </div>
        );
    }
}