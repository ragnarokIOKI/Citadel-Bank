package com.example.bankandroid.RecyclerAdapters;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.bankandroid.R;

import java.util.List;

public class CustomListAdapter extends ArrayAdapter<String> {

    private Context mContext;
    private List<String> itemList;

    public CustomListAdapter(Context context, List<String> itemList) {
        super(context, 0, itemList);
        this.mContext = context;
        this.itemList = itemList;
    }

    @NonNull
    @Override
    public View getView(int position, @Nullable View convertView, @NonNull ViewGroup parent) {
        View listItemView = convertView;
        if (listItemView == null) {
            listItemView = LayoutInflater.from(mContext).inflate(R.layout.user_item, parent, false);
        }

        String currentItem = itemList.get(position);

        ImageView iconImageView = listItemView.findViewById(R.id.icon);
        TextView textTextView = listItemView.findViewById(R.id.text);

        iconImageView.setImageResource(R.drawable.arrow_right);
        textTextView.setText(currentItem);

        return listItemView;
    }
}
