﻿using Neo4jClient;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Relationships.BaseNode
{
    public class LocationRelationship : Relationship, IRelationshipAllowingSourceNode<SchemaNode>
    {
        public LocationRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public LocationRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.Location; }
        }
    }
}