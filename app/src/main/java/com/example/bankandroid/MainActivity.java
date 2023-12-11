package com.example.bankandroid;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;

import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.view.MenuItem;
import android.widget.FrameLayout;

import com.example.bankandroid.R;
import com.example.bankandroid.UserMenus.CardAccountFragment;
import com.example.bankandroid.UserMenus.UserFragment;
import com.google.android.material.bottomnavigation.BottomNavigationView;

public class MainActivity extends AppCompatActivity {
    BottomNavigationView bottomNavigationView;
    FrameLayout frame;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);

        bottomNavigationView = findViewById(R.id.bottom);
        frame = findViewById(R.id.fragment_layout);
        int id = getIntent().getIntExtra("id", 0);

        setFragment(new CardAccountFragment(id));
        bottomNavigationView.setSelectedItemId(R.id.sec);

        bottomNavigationView.setOnNavigationItemSelectedListener(new BottomNavigationView.OnNavigationItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                if (item.getItemId() == R.id.first) {
                    setFragment(new UserFragment());
                } else {
                    if (item.getItemId() == R.id.sec) {
                        setFragment(new CardAccountFragment(id));
                    }
                }
                return false;
            }
        });
    }
    public void setFragment(Fragment fragment){
        getSupportFragmentManager().beginTransaction()
                .replace(R.id.fragment_layout, fragment,null)
                .commit();
    }
}