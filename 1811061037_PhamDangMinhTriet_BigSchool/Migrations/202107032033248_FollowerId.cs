namespace _1811061037_PhamDangMinhTriet_BigSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FollowerId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Followings", name: "Follower_Id", newName: "FollowerId");
            RenameIndex(table: "dbo.Followings", name: "IX_Follower_Id", newName: "IX_FollowerId");
            DropPrimaryKey("dbo.Followings");
            AddPrimaryKey("dbo.Followings", new[] { "FollowerId", "FolloweeId" });
            DropColumn("dbo.Followings", "FollwerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Followings", "FollwerId", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.Followings");
            AddPrimaryKey("dbo.Followings", new[] { "FollwerId", "FolloweeId" });
            RenameIndex(table: "dbo.Followings", name: "IX_FollowerId", newName: "IX_Follower_Id");
            RenameColumn(table: "dbo.Followings", name: "FollowerId", newName: "Follower_Id");
        }
    }
}
