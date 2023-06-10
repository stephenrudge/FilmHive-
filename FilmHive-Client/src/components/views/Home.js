import { Link } from 'react-router-dom';
import "./Home.css"

export const Home = () => {

    return (

        <div>
            <button className="login">
                <Link to="/login">Login/Sign Up</Link>
            </button>
            <h1>FilmHive</h1>
            <img src={require('../../img/download.jpg')} alt='black and white couple facing each other' />
            <img src={require('../../img/hbz-classic-movies-rebecca-gettyimages-2629943-1590707751.jpg')} alt='guy and girl hugging in black and white' />
            <img src={require('../../img/img1.jpg')} alt='old man and young girl facing each other smiling' />
            <div className='image-1'></div>
            <div className='image-2'></div>
            <div className='image-3'></div>
        </div>
    )
}