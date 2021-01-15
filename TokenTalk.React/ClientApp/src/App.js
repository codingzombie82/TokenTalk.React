import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';

import './custom.css'
import { TalksIndex } from './components/Talks/TalksIndex';
import { TalksCreate } from './components/Talks/TalksCreate';
import { TalksEdit } from './components/Talks/TalksEdit';
import { TalksDelete } from './components/Talks/TalksDelete';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
            <Route path='/fetch-data' component={FetchData} />
            <Route path={['/Talks', '/Talks/Index']} component={TalksIndex} exact />
            <Route path='/Talks/Create' component={TalksCreate} exact />
            <Route path='/Talks/Edit/:id' component={TalksEdit} exact/>
            <Route path='/Talks/Delete/:id' component={TalksDelete} exact />
      </Layout>
    );
  }
}
