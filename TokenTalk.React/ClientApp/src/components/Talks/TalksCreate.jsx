﻿import React, { Component } from 'react';
import axios from 'axios';
export class TalksCreate extends Component {

    constructor(props) {
        super(props);

        this.state = {
            title: '',
            description : '',
            created : null
        };
        this.handleChangeTitle = this.handleChangeTitle.bind(this);
        this.handleChangeDescription = this.handleChangeDescription.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.goIndex = this.goIndex.bind(this);
    }
    componentDidMount() {
        
    }
    handleChangeDescription(e){
        this.setState({
            description: e.target.value
        });
    }
    handleChangeTitle(e){
        this.setState({
            title: e.target.value
        });
    }

    goIndex(){
        const { history } = this.props;        
        history.push("/Talks");
    }

    handleSubmit(e){
        e.preventDefault();



        let talkData = {
            title : this.state.title,
            description : this.state.description
        };

        axios.post('/api/Talks', talkData).then(result => {
            this.goIndex();
        });
    }

    render() {

        return (
            <div>
                <h1>TalksCreate</h1>

                <div className="row">
                    <div className="col-md-8">
                        <form onSubmit={this.handleSubmit}>
                            <div className="form-group">
                                <label>Title</label>
                                <input 
                                type="text" 
                                className="form-control" 
                                value={this.state.title} 
                                placeholder="제목을 입력하세요"
                                onChange={this.handleChangeTitle}
                                ></input>
                            </div>
                            <div className="form-group">
                                <label>Description</label>
                                <input 
                                type="text" 
                                className="form-control" 
                                value={this.state.description} 
                                placeholder="내용을 입력하세요"
                                onChange={this.handleChangeDescription}
                                ></input>
                            </div>
                            <div className="form-group">
                                <button className="btn btn-secondary" onClick={this.goIndex}>List</button>
                                &nbsp;
                                <button className="btn btn-primary" type="submit">저장</button>
                            </div>
                        </form>                        
                    </div>
                </div>
            </div>
        );
    }
}