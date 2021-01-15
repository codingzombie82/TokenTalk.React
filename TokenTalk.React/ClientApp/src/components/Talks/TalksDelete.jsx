import React, { Component } from 'react';
import axios from 'axios';
export class TalksDelete extends Component {

    constructor(props) {
        super(props);

        this.state = {
            title: '',
            description: '',
            created: null
        };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.goIndex = this.goIndex.bind(this);
    }

    componentDidMount() {
        const { id } = this.props.match.params;
        // id 값에 해당하는 단일 데이터를 web api로 부터 읽기
        console.log(id);

        axios.get("/api/Talks/" + id).then(response => {
            const data = response.data;

            this.setState({
                title: data.title,
                description: data.description,
                created: data.created
            });
        });
    }


    goIndex() {
        const { history } = this.props;
        history.push("/Talks");
    }

    handleSubmit(e) {
        e.preventDefault();

        if (window.confirm("삭제하시겠습니까?")) {
            const { id } = this.props.match.params;

            axios.delete('/api/Talks/' + id).then(result => {
                this.goIndex();
            });
        } else {
            return false;
        }
    }

    render() {

        return (
            <div>
                <h1>TalksDelete</h1>

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
                                    readOnly                                    
                                ></input>

                            </div>
                            <div className="form-group">
                                <label>Description</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    value={this.state.description}
                                    placeholder="내용을 입력하세요"
                                    readOnly    
                                ></input>
                            </div>
                            <div className="form-group">
                                <button className="btn btn-secondary" onClick={this.goIndex}>리스트</button>
                                &nbsp;
                                <button className="btn btn-danger" type="submit">삭제</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        );
    }
}