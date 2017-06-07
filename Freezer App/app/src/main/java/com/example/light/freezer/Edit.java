package com.example.light.freezer;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.CheckBox;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.List;


public class Edit extends Activity {


    List<AppsItemInfo> list;
    AppUsageStatisticsFragment white = new AppUsageStatisticsFragment();

    private ListView listView;
    private PackageManager pManager;
    //private ComponentName componet;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        // TODO Auto-generated method stub
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_edit);


        // 取得ListView
        listView = (ListView) findViewById(R.id.edit_list);

        // 获取图片、应用名、包名
        pManager = Edit.this.getPackageManager();
        List<PackageInfo> appList = getAllApps(Edit.this);

        list = new ArrayList<AppsItemInfo>();

        for (int i = 0; i < appList.size(); i++) {
            PackageInfo pinfo = appList.get(i);
            AppsItemInfo shareItem = new AppsItemInfo();

            // 设置图片
            shareItem.setIcon(pManager
                    .getApplicationIcon(pinfo.applicationInfo));
            // 设置应用程序名字
            shareItem.setLabel(pManager.getApplicationLabel(
                    pinfo.applicationInfo).toString());
            // 设置应用程序的包名
            shareItem.setPackageName(pinfo.applicationInfo.packageName);

            for(int j =0 ; j <white.other.size(); j++){

                if(shareItem.getPackageName().equals(white.other.get(j).usageStats.getPackageName())){
                    shareItem.setCheckBox("T");
                }
                else{
                    shareItem.setCheckBox("F");
                }

            }


            list.add(shareItem);

        }

        // 设置gridview的Adapter
        listView.setAdapter(new baseAdapter());

        // 点击应用图标时，做出响应
        listView.setOnItemClickListener(new ClickListener());
/**
        Button btn_complete = (Button)findViewById(R.id.btn_complete);
        btn_complete.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {


                for(int i =0;i<list.size();i++){


                        if(list.get(i).getCheck().equals("T")){

                            Intent intent = getPackageManager().getLaunchIntentForPackage(list.get(i).getPackageName());
                            intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                            startActivity(intent);

                        }
                }

                finish();


            }
        });
**/



    }

    public static List<PackageInfo> getAllApps(Context context) {

        List<PackageInfo> apps = new ArrayList<PackageInfo>();
        PackageManager pManager = context.getPackageManager();
        // 获取手机内所有应用
        List<PackageInfo> packlist = pManager.getInstalledPackages(0);
        for (int i = 0; i < packlist.size(); i++) {
            PackageInfo pak = (PackageInfo) packlist.get(i);

            // 判断是否为非系统预装的应用程序
            // 这里还可以添加系统自带的，这里就先不添加了，如果有需要可以自己添加
            // if()里的值如果<=0则为自己装的程序，否则为系统工程自带
            if ((pak.applicationInfo.flags & pak.applicationInfo.FLAG_SYSTEM) <= 0) {
                // 添加自己已经安装的应用程序
                apps.add(pak);
            }

        }
        return apps;
    }

    private class baseAdapter extends BaseAdapter {
        LayoutInflater inflater = LayoutInflater.from(Edit.this);

        @Override
        public int getCount() {
            // TODO Auto-generated method stub
            return list.size();
        }

        @Override
        public Object getItem(int position) {
            // TODO Auto-generated method stub
            return null;
        }

        @Override
        public long getItemId(int position) {
            // TODO Auto-generated method stub
            return position;
        }

        @Override
        public View getView(int position, View convertView, ViewGroup parent) {
            // TODO Auto-generated method stub
            ViewHolder holder;
            if (convertView == null) {
                // 使用View的对象itemView与R.layout.item关联
                convertView = inflater.inflate(R.layout.usage_row_edit, null);
                holder = new ViewHolder();
                holder.icon = (ImageView) convertView
                        .findViewById(R.id.iv_app_icon);
                holder.label = (TextView) convertView
                        .findViewById(R.id.tv_app_name);
                holder.check = (CheckBox) convertView
                        .findViewById(R.id.edit_check);

                convertView.setTag(holder);
            } else {
                holder = (ViewHolder) convertView.getTag();
            }


            holder.icon.setImageDrawable(list.get(position).getIcon());
            holder.label.setText(list.get(position).getLabel().toString());
            if(list.get(position).getCheck().equals("T")){
                holder.check.setChecked(true);
            }
            else{
                holder.check.setChecked(false);
            }




            return convertView;

        }

    }

    private class ViewHolder{
        private ImageView icon;
        private TextView label;
        private CheckBox check;
    }

    // 当用户点击应用程序图标时，将对这个类做出响应
    private class ClickListener implements AdapterView.OnItemClickListener {

        @Override
        public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,
                                long arg3) {

//			// 将应用所选的应用程序信息共享到Application中
//		MyApp appState = ((MyApp) getApplicationContext());
//			// 获取当前所在选项卡
//			String tab_id = appState.getTab_id();
//			// 设置所选应用程序信息
//			appState.set_AppInfo(tab_id, list.get(arg2).getLabel(), list.get(
//
// 		arg2).getIcon(), list.get(arg2).getPackageName());


            // String pkg = res.AppsItemInfo.packageName;


            //String cls = res.activityInfo.name;
            Intent intent = new Intent();
            intent = Edit.this.getPackageManager().
                    getLaunchIntentForPackage(list.get(arg2).getPackageName());

            intent.addFlags(Intent.FLAG_ACTIVITY_NO_USER_ACTION);
            startActivity(intent);
            // 销毁当前Activity
            //finish();
        }

    }
    class AppsItemInfo {

        private Drawable icon;
        private String label;
        private String packageName;
        private String check;

        public Drawable getIcon() {
            return icon;
        }

        public void setIcon(Drawable icon) {
            this.icon = icon;
        }

        public String getLabel() {
            return label;
        }

        public void setLabel(String label) {
            this.label = label;
        }

        public String getPackageName() {
            return packageName;
        }

        public void setPackageName(String packageName) {
            this.packageName = packageName;
        }

        public String getCheck() {
            return check;
        }

        public void setCheckBox(String check) {
            this.check = check;
        }


    }

}
