<?php

use Illuminate\Database\Seeder;

class UserTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        //DB::table('users')->truncate();
        DB::table('users')->insert([
            [
                'firstname' => 'Admin',
                'lastname' => 'Root',
                'email' => 'test@host.local',
                'password' => bcrypt('secret'),
                'admin' => '1'
            ]
        ]);
    }
}
