import React from "react";
import "./books.css";

const Library: React.FC = () => {
    return (
        <main>
            <section className="library">
                <div className="container">
                    <h2 className="library-title">Library</h2>

                    <div className="search-box">
                        <input type="text" placeholder="Search Book..." />
                        <button className="search-btn">
                            <img src="./images/search_icon.svg" alt="search" />
                        </button>
                    </div>

                    <ul className="library-list">
                        <li className="library-item">
                            <div className="book">
                                <div className="book-image">
                                    <img
                                        src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSNHV0HnZFjnPVeDJnh_jxfSaaxOJaazyc6Kw&s"
                                        alt="Fight Oligarchy"
                                    />
                                </div>

                                <div className="book-info">
                                    <h3 className="book-title">Fight Oligarchy</h3>
                                    <p className="book-author">Author: Bernie Sanders</p>
                                    <strong>Pages: 300</strong>
                                </div>
                            </div>
                        </li>

                        <li className="library-item">
                            <div className="book">
                                <div className="book-image">
                                    <img
                                        src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSNHV0HnZFjnPVeDJnh_jxfSaaxOJaazyc6Kw&s"
                                        alt="You've Reached Sam"
                                    />
                                </div>

                                <div className="book-info">
                                    <h3 className="book-title">You've Reached Sam</h3>
                                    <p className="book-author">Author: Bernie Sanders</p>
                                    <strong>Pages: 200</strong>
                                </div>
                            </div>
                        </li>

                        </ul>
                </div>
            </section>
        </main>
    );
};

export default Library;
