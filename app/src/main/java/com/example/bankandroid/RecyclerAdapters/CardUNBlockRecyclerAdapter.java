package com.example.bankandroid.RecyclerAdapters;

import android.annotation.SuppressLint;
import android.content.Context;
import android.support.annotation.NonNull;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import com.example.bankandroid.Models.Card;
import com.example.bankandroid.R;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class CardUNBlockRecyclerAdapter extends RecyclerView.Adapter<CardUNBlockRecyclerAdapter.CardUNBlockVH>{
    Context context;
    public ArrayList<Card> list = new ArrayList<Card>();
    public Map<Integer, Boolean> checked = new HashMap<>();

    public CardUNBlockRecyclerAdapter(Context context, ArrayList<Card> list) {
        this.context = context;
        this.list = list;
    }

    @NonNull
    @Override
    public CardUNBlockVH onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(context).inflate(R.layout.block_cardaccount_item, parent, false);
        return new CardUNBlockRecyclerAdapter.CardUNBlockVH(v);
    }

    @Override
    public void onBindViewHolder(@NonNull CardUNBlockRecyclerAdapter.CardUNBlockVH holder,  @SuppressLint("RecyclerView") int position) {
        Card card = list.get(position);
        holder.txt_number.setText(card.getIdCard());

        holder.checkbox.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                checked.put(position, isChecked);
            }
        });
    }

    @Override
    public int getItemCount() {
        return list.size();
    }

    public static class CardUNBlockVH extends RecyclerView.ViewHolder {
        public TextView txt_number;
        public CheckBox checkbox;

        public CardUNBlockVH(@NonNull View itemView) {
            super(itemView);
            txt_number = itemView.findViewById(R.id.numberAccCard);
            checkbox = itemView.findViewById(R.id.checkboxCard);
        }
    }
}
