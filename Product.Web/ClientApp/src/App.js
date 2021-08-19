import React, { Component } from 'react';
import { Route } from 'react-router';
import { ErrorBoundary } from './ErrorBoundary';
import { Layout } from './components/Layout';
import { Products } from './components/Product/Products';
import { UpsertProduct } from './components/Product/UpsertProduct';

import './custom.css'

export default class App extends Component {
	static displayName = App.name;

	render() {
		return (
			<ErrorBoundary>
			<Layout>
				<Route exact path='/' component={Products} />
				<Route path='/addProduct' component={UpsertProduct} />
				<Route path='/editProduct/:id' component={UpsertProduct} />
				</Layout>
				</ErrorBoundary>
		);
	}
}
