import React, { Component } from 'react';
import LayoutContext from '../LayoutContext';
import { FormErrors }   from '../FormError';

export class UpsertProduct extends Component {
	static displayName = UpsertProduct.name;
	static contextType = LayoutContext;

	constructor(props) {
		super(props);
		this.state = {
			product: {}, loading: true, mode: this.props.match.params.id === undefined ? "add" : "edit",
			types: ["Books", "Electronics", "Food", "Furniture", "Toys"],
			formErrors: { name: '', price: '', type: '' },
			nameValid: false,
			priceValid: false,
			typeValid: false,
			formValid: false
		};
	}

	componentDidMount() {
		if (this.state.mode === "edit") {
			this.populateProductData(this.props.match.params.id);
		} else {
			var product = { name: "", price: 0, type: "", active: false };
			this.setState({ product, loading: false });
		}
	}

	handleChange = (value) => (prop) => {
		console.log(value);
		var p = { ...this.state.product }
		p[prop] = value
		console.log(p);
		this.setState({ product: p }, () => { this.validateField(prop, value) });
	}

	validateField(fieldName, value) {
		let fieldValidationErrors = this.state.formErrors;
		let nameValid = this.state.nameValid;
		let priceValid = this.state.priceValid;
		let typeValid = this.state.typeValid;

		switch (fieldName) {
			case 'name':
				nameValid = value.length > 0;
				fieldValidationErrors.name = nameValid ? '' : ' is invalid';
				break;
			case 'price':
				priceValid = value >= 0;
				fieldValidationErrors.price = priceValid ? '' : ' is invalid';
				break;
			case 'type':
				typeValid = this.state.types.includes(value);
				fieldValidationErrors.type = typeValid ? '' : ' is invalid';
				break;
			default:
				break;
		}
		this.setState({
			formErrors: fieldValidationErrors,
			nameValid: nameValid,
			priceValid: priceValid,
			typeValid: typeValid
		}, this.validateForm);
	}

	validateForm() {
		this.setState({ formValid: this.state.nameValid && this.state.priceValid && this.state.typeValid });
	}

	errorClass(error) {
		return (error.length === 0 ? '' : 'has-error');
	}

	onSubmit = async (e) => {
		e.preventDefault();
		if (this.state.mode === "edit") {
			await this.saveProductData();
		} else {
			await this.createProductData();
		}
	}

	onBack = async () => {
		this.props.history.push("/");
	}


	renderProduct = (product) => {
		return (
			<div>
	
				<form  noValidate>
					<div className="panel panel-default">
						<FormErrors formErrors={this.state.formErrors} />
					</div>
					<div className={`form-group col-6 ${this.errorClass(this.state.formErrors.name)}`} >
						<label htmlFor="productName">Product Name</label>
						<input type="text" className="form-control" id="name" placeholder="Name" required
							value={product.name}
							onChange={(e) => { this.handleChange(e.target.value)("name"); }}

						/>
					</div>
					<div className={`form-group col-6 ${this.errorClass(this.state.formErrors.name)}`}>
						<label htmlFor="productName">Price</label>
						<input type="text" className="form-control" id="price" placeholder="Price"
							value={product.price}
							onChange={(e) => { this.handleChange(e.target.value)("price"); }}

						/>
					</div>
					<div className={`form-group col-6 ${this.errorClass(this.state.formErrors.name)}`}>
						<select className="select-css"
							value={product.type}
							onChange={(e) => {
								this.handleChange(e.target.value)("type");
							}}
						>
							<option defaultValue>Choose Type of Product</option>
							{this.state.types.map((type) =>
								<option key={type} value={type}>{type}</option>
							)}
						</select>
					</div>
					<div className="form-group col-6">
						<label className="switch">
							<input type="checkbox" checked={product.active}
								onChange={(e) => { this.handleChange(!this.state.product.active)("active"); }}

							/>t
						<span className="slider round"></span>
						</label>
					</div>
					<div className="form-group row">
						<div className="col-3">
							<button className="btn btn-primary" disabled={!this.state.formValid}
								onClick={(e) => { this.onSubmit(e) }} type="submit">Save</button>
						</div>
						<div className="col-3">
							<button className="btn btn-primary" onClick={() => { this.onBack() }}>Back</button>
						</div>
					</div>
				</form>
			</div>
		);
	}

	render() {
		let contents = this.state.loading
			? <p><em>Loading...</em></p>
			: this.renderProduct(this.state.product);

		return (
			<div>
				<h1 id="tabelLabel" >Product</h1>
				{contents}
			</div>
		);
	}

	async populateProductData(id) {
		try {
			const response = await fetch('Product/' + id);
			const data = await response.json();
			console.log(data);
			this.setState({
				product: data, loading: false,
				formValid: true,
				nameValid: true,
				priceValid: true,
				typeValid: true, });
		} catch (error) {
			console.log('Fetch error: ', error);
		}
	}

	async saveProductData() {
		try {
			const requestOptions = {
				method: 'PUT',
				headers: { 'Content-Type': 'application/json' },
				body: JSON.stringify({
					Id: this.state.product.id,
					Name: this.state.product.name,
					Price: Number(this.state.product.price),
					Type: this.state.product.type,
					Active: !!this.state.product.active
				})
			};
			const response = await fetch('Product/' + this.state.product.id, requestOptions);
			console.log(response);
			this.context.notify("saved product " + this.state.product.name);
			//const data = await response.json();
		} catch (error) {
			console.log('Fetch error: ', error);
		}
	}

	async createProductData() {
		try {
			const requestOptions = {
				method: 'POST',
				headers: { 'Content-Type': 'application/json' },
				body: JSON.stringify({
					Name: this.state.product.name,
					Price: Number(this.state.product.price),
					Type: this.state.product.type,
					Active: !!this.state.product.active
				})
			};
			console.log(requestOptions.body);
			const response = await fetch('Product/', requestOptions);
			const data = await response.json();
			console.log(response);
			if (response.status === 201) {
				this.context.notify("created product " + this.state.product.name);
				var p = { ...this.state.product, id: data.id };
				this.setState({ product: p });
				this.props.history.push("/EditProduct/" + p.id);
			}
		} catch (error) {
			console.log('Fetch error: ', error);
		}
	}
}
