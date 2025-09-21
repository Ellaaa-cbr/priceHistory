import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer } from 'recharts';
import React from 'react'

export default function PriceChart({data}){
    if (!data || data.length === 0) {
        return <div className="card chart-card">no data</div>
    }
    return(
        <div className='card chart-card' style={{height: 360}}>
            <ResponsiveContainer width="100%" height="100%">
                <LineChart data={data} margin={{ top: 12, right: 24, left: 8, bottom: 8 }}>
                    <CartesianGrid strokeDasharray="3 3"/>
                        <XAxis dataKey="date" />
                        <YAxis />
                        <Tooltip />
                        <Line type="monotone" dataKey="price" dot={false} strokeWidth={2} />
                </LineChart>
            </ResponsiveContainer>

        </div>
    )
}