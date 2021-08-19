import React, { Component } from 'react';
import { confirmAlert } from 'react-confirm-alert'; 
import 'react-confirm-alert/src/react-confirm-alert.css'; 
import { Pagination } from '../Pagination';

export class Products extends Component {
	static displayName = Products.name;

	constructor(props) {
		super(props);
		this.state = {
			products: [], loading: true, pagination: {} };
	}

	componentDidMount() {
		this.populateProductData('Product');
	}

	handlePaginationapi = (api) => {
		console.log(api);
		this.populateProductData(api);
	}

	handleClickAdd = () => {
		console.log("Add");
		this.props.history.push("/AddProduct/");
	}

	handleClickEdit = (id) => {
		console.log(id);
		this.props.history.push("/EditProduct/" + id);
	}

	handleClickDelete = (id) => (name) => {
		confirmAlert({
			title: 'Confirm to Delete',
			message: 'Are you sure you wish to delete '+ name + ' ?',
			buttons: [
				{
					label: 'Yes',
					onClick: () => this.createProductData(id)
				},
				{
					label: 'No'
				}
			]
		});
	}

	handleSort = (col) => {
		console.log(col);
		this.populateProductData('product?sort=' + col);
	}

	renderProducts = (products) => {
		return (
			<div>
				<button className="btn btn-primary" onClick={() => this.handleClickAdd()} >Add</button>
				<table className='table table-striped' aria-labelledby="tabelLabel">
					<thead>
						<tr>
							<th onClick={() => this.handleSort("name")}>Name</th>
							<th onClick={() => this.handleSort("price")}>Price</th>
							<th onClick={() => this.handleSort("type")}>Type</th>
							<th onClick={() => this.handleSort("active")}>Active</th>
							<th></th>
							<th></th>
						</tr>
					</thead>
					<tbody>
						{products.map(product =>
							<tr key={product.id}>
								<td>{product.name}</td>
								<td>{product.price}</td>
								<td>{product.type}</td>
								<td>{product.active === true ? 'Yes' : 'No'}</td>
								<td><button className="btn btn-primary" onClick={() => this.handleClickEdit(product.id)} >Edit</button></td>
								<td><button className="btn btn-primary" onClick={() => this.handleClickDelete(product.id)(product.name)}>Delete</button>
								</td>
							</tr>
						)}
					</tbody>
				</table>
				<Pagination value={this.state.pagination} onChangePaginationapi={this.handlePaginationapi}/>
			</div>
		);
	}

	render() {
		let contents = this.state.loading
			? <p><em>Loading...</em></p>
			: this.renderProducts(this.state.products);

		return (
			<div>
				<h1 id="tabelLabel" >Products</h1>
				{contents}
			</div>
		);
	}

	async populateProductData(api) {
		try {
			const response = await fetch(api);
			console.log(response);
			let pagination = JSON.parse(response.headers.get('X-Pagination'));
			console.log(pagination);
			const data = await response.json();
			this.setState({ products: data, loading: false, pagination });
		} catch (error) {
			console.log('Fetch error: ', error);
		}
	}

	async createProductData(id) {
		try {
			const requestOptions = {
				method: 'DELETE',
				headers: { 'Content-Type': 'application/json' }
			};
			const response = await fetch('Product/' + id, requestOptions);
			console.log(response);
			if (response.status === 204) {
				let data = this.state.products.filter((p) => p.id !== id);
				console.log(data);
				this.setState({ products: data });
			}

		} catch (error) {
			console.log('Fetch error: ', error);
		}
	}
}
