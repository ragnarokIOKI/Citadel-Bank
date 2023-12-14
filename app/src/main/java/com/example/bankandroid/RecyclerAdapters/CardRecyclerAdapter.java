package com.example.bankandroid.RecyclerAdapters;

import android.annotation.SuppressLint;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.core.content.ContextCompat;
import androidx.recyclerview.widget.RecyclerView;

import com.example.bankandroid.Models.Card;
import com.example.bankandroid.R;

import java.util.ArrayList;

public class CardRecyclerAdapter extends RecyclerView.Adapter<CardRecyclerAdapter.CardVH> {
    Context context;
    public ArrayList<Card> list = new ArrayList<Card>();
    int count;

    public CardRecyclerAdapter(Context context, ArrayList<Card> list) {
        this.context = context;
        this.list = list;
    }

    @NonNull
    @Override
    public CardVH onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View v = LayoutInflater.from(context).inflate(R.layout.card_item, parent, false);
        return new CardVH(v);
    }

    @Override
    public void onBindViewHolder(@NonNull CardVH holder, @SuppressLint("RecyclerView") int position) {
        Card card = list.get(position);
        holder.txt_number.setText(card.getIdCard());
        holder.txt_holder.setText(card.getCardHolder());
        holder.txt_val.setText(card.getValidity());
        holder.txt_ccv.setText("***");

        char firstDigit = holder.txt_number.getText().charAt(0);

        if (firstDigit == '4') {
            holder.type.setImageDrawable(ContextCompat.getDrawable(context, R.drawable.visa));
        } else if (firstDigit == '3') {
            holder.type.setImageDrawable(ContextCompat.getDrawable(context, R.drawable.mastercard));
        } else if (firstDigit == '2') {
            holder.type.setImageDrawable(ContextCompat.getDrawable(context, R.drawable.mir));
        }

        if (card.isCardDeleted() != true) {
            holder.txt_number.setText("Ваша банковская карта заблокирована.");
            holder.txt_ccv.setText("---");
            holder.txt_holder.setText("");
            if (card.getCard_Deletion_Reason() != null) {
                holder.txt_val.setText("Причина блокировки:" + card.getCard_Deletion_Reason());
            } else {
                holder.txt_val.setText("Причина блокировки: Заблокировано администратором.");
            }
        }

        holder.txt_ccv.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                count++;
                if (count % 2 == 0){
                    holder.txt_ccv.setText(card.getCcv());
                } else {
                    holder.txt_ccv.setText("***");
                }
                if (card.isCardDeleted() != true) {
                    holder.txt_ccv.setText("---");
                }
            }
        });
    }

    @Override
    public int getItemCount() {
        return list.size();
    }

    public static class CardVH extends RecyclerView.ViewHolder
    {
        public TextView txt_number, txt_holder, txt_val, txt_ccv;
        public ImageView type;
        public CardVH(@NonNull View itemView)
        {
            super(itemView);
            txt_number = itemView.findViewById(R.id.tvCardNumber);
            txt_holder = itemView.findViewById(R.id.tvCardHolder);
            txt_val = itemView.findViewById(R.id.tvExpiry);
            txt_ccv = itemView.findViewById(R.id.ccvText);
            type = itemView.findViewById(R.id.cardtypeIM);
        }
    }
}
