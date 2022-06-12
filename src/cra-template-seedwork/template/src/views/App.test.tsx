import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';
import { getDetaultStore } from '../domain/RootStoreModel';

test('renders hello, world', () => {
  render(<App store={getDetaultStore()} />);
  const element = screen.getByText(/Hello, World/i);
  expect(element).toBeInTheDocument();
});
