import React from 'react';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend } from 'recharts';

interface Props {
  data: any;
  simarity: number[];
  items: any[];
}
const Histogram = (props: Props) => {
  const totalWords = props.data?.scannedDocument?.totalWords as any;
  const results = props.data.results.batch.map((x: any) => ({value: (x.matchedWords/totalWords)*100, name: x.metadata.filename}));

  const data = props.simarity.map((x, index) => ({value: x*100, name: `File ${index+1}`}))
  return (
    <div style={{ display: 'flex', marginLeft: '300px', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
      <h2>Similarity score</h2>
      <BarChart
        width={600}
        height={400}
        data={data}
        margin={{ top: 20, right: 30, left: 20, bottom: 5 }}
      >
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis dataKey="name" />
        <YAxis />
        <Tooltip />
        <Legend />
        <Bar dataKey="value" fill="#8884d8" />
      </BarChart>
    </div>
  );
};

export default Histogram;