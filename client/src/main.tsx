import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './app/App.tsx'
import 'bootstrap/scss/bootstrap.scss'
import 'bootstrap-icons/font/bootstrap-icons.css';
import './main.scss'
ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
)
